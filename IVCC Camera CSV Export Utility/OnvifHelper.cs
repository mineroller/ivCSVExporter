using IVCC_Camera_CSV_Export_Utility.OnvifDeviceMgmt10;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Collections.Generic;
using System.Linq;

namespace IVCC_Camera_CSV_Export_Utility
{
    class OnvifHelper
    {
        public static CustomBinding CreateCustomBinding()
        {
            // References:
            // SO 1: https://stackoverflow.com/questions/32779467/onvif-api-capture-image-in-c-sharp
            // SO 2:
            // Bullet-Time-ONVIF: https://github.com/mrrekcuf/Bullet-Time-ONVIF             

            // Required to prevent 417 Expectation Failed error

            ServicePointManager.Expect100Continue = false;

            var msgElement = new TextMessageEncodingBindingElement();
            msgElement.MessageVersion = MessageVersion.CreateVersion(EnvelopeVersion.Soap12, AddressingVersion.None);

            // Initial binding for posting to device_service, with Digest authentication
            HttpTransportBindingElement bindElement = new HttpTransportBindingElement();
            bindElement.AuthenticationScheme = AuthenticationSchemes.Digest;
            CustomBinding _customBind = new CustomBinding(msgElement, bindElement);

            return _customBind;
        }

        public static DeviceClient GetONVIFDevice(CustomBinding _cb, string _ep)
        {
            // Create a new Device, the get the list of available service.
            try
            {
                DeviceClient _device = new DeviceClient(_cb, new EndpointAddress(_ep));

                return _device;
            }
            catch
            {
                throw;
            }
        }

        public static ivOnvifObject CreateONVIFObject(string _dsAddress, string _usr, string _pwd)
        {
            CustomBinding customBind = CreateCustomBinding();
            DeviceClient device = GetONVIFDevice(customBind, _dsAddress);

            device.ClientCredentials.HttpDigest.ClientCredential.UserName = _usr;
            device.ClientCredentials.HttpDigest.ClientCredential.Password = _pwd;
            device.ClientCredentials.HttpDigest.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            
            // Get Model, Firmware and Serial

            string _model;
            string _firmware;
            string _serialNumber;
            string _HardwareId;
            
            device.GetDeviceInformation(out _model, out _firmware, out _serialNumber, out _HardwareId);                        
            
            // Get Location  (_locDecoded)

            Scope[] scopes = device.GetScopes();
            List<string> scopeItems = new List<string>();

            foreach (Scope s in scopes)
            {
                scopeItems.Add(s.ScopeItem);
            }
            
            string _locString = scopeItems.First(s => s.Contains("location"));
            string _locDecoded = WebUtility.UrlDecode(_locString.Substring(31));

            // Get MAC Address

            NetworkInterface[] interfaces = device.GetNetworkInterfaces();
            List<string> nIntItems = new List<string>();
            
            foreach (NetworkInterface n in interfaces)
            {
                nIntItems.Add(n.Info.HwAddress);
            }

            // Assume NetworkInterface will only contain 1 item for IP camera
            string _macString = nIntItems[0];

            // Create OnvifObject

            ivOnvifObject obj = new ivOnvifObject
            {
                FW_Version = _firmware,
                Hardware_Model = _model,
                Location = _locDecoded,
                MAC_Address = _macString,
                Serial_Number = _serialNumber,
            };

            return obj;

        }


    }    
}
