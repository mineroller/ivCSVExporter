using IVCC_Camera_CSV_Export_Utility.OnvifDeviceMgmt10;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Collections.Generic;
using System.Linq;
using IVCC_Camera_CSV_Export_Utility.OnvifMedia20;
using System.IO;

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

            OnvifDeviceMgmt10.NetworkInterface[] interfaces = device.GetNetworkInterfaces();
            List<string> nIntItems = new List<string>();

            foreach (OnvifDeviceMgmt10.NetworkInterface n in interfaces)
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

        public static string GetSnapshotURI(string _dsAddress, string _usr, string _pwd, int _profileNumber)
        {

            CustomBinding customBind = CreateCustomBinding();
            DeviceClient device = GetONVIFDevice(customBind, _dsAddress);
            Service[] services = device.GetServices(false);

            // Then, find the Media related service by LINQ call

            Service media = services.FirstOrDefault(s => s.Namespace == "http://www.onvif.org/ver20/media/wsdl");

            Media2Client sabM2Client = new Media2Client(customBind, new EndpointAddress(media.XAddr));

            sabM2Client.ClientCredentials.HttpDigest.ClientCredential.UserName = _usr;
            sabM2Client.ClientCredentials.HttpDigest.ClientCredential.Password = _pwd;
            sabM2Client.ClientCredentials.HttpDigest.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

            // Finally get a list of capabilities from this device.
            // For this, we are interested in if SnapshotUri is true

            Capabilities2 cap2 = sabM2Client.GetServiceCapabilities();

            if (cap2.SnapshotUri)
            {
                // Get list of profiles, then select the one provided by default
                try
                {
                    MediaProfile[] profiles = sabM2Client.GetProfiles(null, null);
                    string _uri = sabM2Client.GetSnapshotUri(profiles[_profileNumber].token);
                    return _uri;
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                return "NA";
            }
        }

        public static MemoryStream DownloadSnapshot(string _uri, string _onvifuser, string _onvifpassword)
        {
            WebClient _wc = new WebClient();
            _wc.Credentials = new NetworkCredential(_onvifuser, _onvifpassword);
            MemoryStream _ms = new MemoryStream(_wc.DownloadData(_uri));
            return _ms;
        }
    }
}
