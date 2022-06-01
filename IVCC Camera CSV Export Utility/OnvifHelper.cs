using IVCC_Camera_CSV_Export_Utility.OnvifDeviceMgmt10;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;

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

        public static string GetONVIFDeviceInfo(string _dsAddress, string _usr, string _pwd)
        {
            CustomBinding customBind = CreateCustomBinding();
            DeviceClient device = GetONVIFDevice(customBind, _dsAddress);

            device.ClientCredentials.HttpDigest.ClientCredential.UserName = _usr;
            device.ClientCredentials.HttpDigest.ClientCredential.Password = _pwd;
            device.ClientCredentials.HttpDigest.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

            string _model;
            string _firmware;
            string _serialNumber;
            string _HardwareId;

            try
            {
                device.GetDeviceInformation(out _model, out _firmware, out _serialNumber, out _HardwareId);                
                string _info = _model + " | " + _firmware + " | " + _serialNumber;
                return _info;
            }
            catch
            {
                throw;
            }

        }

    }
}
