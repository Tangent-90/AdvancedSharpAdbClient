using AdvancedSharpAdbClient.SampleApp.Data;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;

namespace AdvancedSharpAdbClient.SampleApp.Helpers
{
    /// <summary>
    /// Class providing functionality to support generating and copying protocol activation URIs.
    /// </summary>
    public static class ProtocolActivationClipboardHelper
    {
        private const string ShowCopyLinkTeachingTipKey = "ShowCopyLinkTeachingTip";

        public static bool ShowCopyLinkTeachingTip
        {
            get
            {
                object valueFromSettings = ApplicationData.Current.LocalSettings.Values[ShowCopyLinkTeachingTipKey];
                if (valueFromSettings == null)
                {
                    ApplicationData.Current.LocalSettings.Values[ShowCopyLinkTeachingTipKey] = true;
                    valueFromSettings = true;
                }
                return (bool)valueFromSettings;
            }

            set => ApplicationData.Current.LocalSettings.Values[ShowCopyLinkTeachingTipKey] = value;
        }

        public static void Copy(ControlInfoDataItem item)
        {
            Uri uri = new($"winui2gallery://item/{item.UniqueId}", UriKind.Absolute);
            ProtocolActivationClipboardHelper.Copy(uri, $"{Package.Current.DisplayName} - {item.Title} Sample");
        }

        public static void Copy(ControlInfoDataGroup group)
        {
            Uri uri = new($"winui2gallery://category/{group.UniqueId}", UriKind.Absolute);
            ProtocolActivationClipboardHelper.Copy(uri, $"{Package.Current.DisplayName} - {group.Title} Samples");
        }

        private static void Copy(Uri uri, string displayName)
        {
            string htmlFormat = HtmlFormatHelper.CreateHtmlFormat($"<a href='{uri}'>{displayName}</a>");

            DataPackage dataPackage = new();
            dataPackage.SetApplicationLink(uri);
            dataPackage.SetWebLink(uri);
            dataPackage.SetText(uri.ToString());
            dataPackage.SetHtmlFormat(htmlFormat);

            Clipboard.SetContentWithOptions(dataPackage, null);
        }
    }
}
