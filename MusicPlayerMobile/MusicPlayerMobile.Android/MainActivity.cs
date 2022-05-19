namespace MusicPlayerMobile.Droid
{
    using Android;
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Android.Widget;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    ///     The main activity.
    /// </summary>
    [Activity(Label = "MusicPlayerMobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /// <summary>
        ///     The read external storage permission.
        /// </summary>
        const string ReadExternalStoragePermission = Manifest.Permission.ReadExternalStorage;

        ///// <summary>
        /////     The write external storage permission.
        ///// </summary>
        //const string WriteExternalStoragePermission = Manifest.Permission.WriteExternalStorage;

        /// <summary>
        ///     The request stroage permission identifier.
        /// </summary>
        private const int RequestExternalStoragePermissionId = 1;

        ///// <summary>
        /////     The request stroage permission identifier.
        ///// </summary>
        //private const int RequestWriteExternalStoragePermissionId = 2;

        /// <summary>
        ///     The permissions.
        /// </summary>
        private readonly string[] Permissions = { ReadExternalStoragePermission };

        /// <inheritdoc/>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            this.VerifyPermissions();

            base.OnCreate(savedInstanceState);

            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            this.LoadApplication(new App());
        }

        /// <inheritdoc/>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestExternalStoragePermissionId:
                    {
                        if (grantResults[0] == (int)Permission.Granted)
                        {
                            Toast.MakeText(this, "Read Permission Granted", ToastLength.Short).Show();
                        }
                        else
                        {
                            Toast.MakeText(this, "Read Permission Denied", ToastLength.Short).Show();
                        }
                    }

                    break;

                    //case RequestWriteExternalStoragePermissionId:
                    //    {
                    //        if (grantResults[1] == (int)Permission.Granted)
                    //        {
                    //            Toast.MakeText(this, "Write Permission Granted", ToastLength.Short).Show();
                    //        }
                    //        else
                    //        {
                    //            Toast.MakeText(this, "Write Permission Denied", ToastLength.Short).Show();
                    //        }
                    //    }

                    //    break;
            }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        ///     Verifies if permissions are needed.
        /// </summary>
        private void VerifyPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                this.GetPermissions();
                return;
            }
        }

        /// <summary>
        ///     Gets all required permissions to access the device data.
        /// </summary>
        private void GetPermissions()
        {
            if (this.CheckSelfPermission(ReadExternalStoragePermission) != (int)Permission.Granted)
            {
                this.RequestPermissions(this.Permissions, RequestExternalStoragePermissionId);
            }

            //if (this.CheckSelfPermission(WriteExternalStoragePermission) != (int)Permission.Granted)
            //{
            //    this.RequestPermissions(this.Permissions, RequestWriteExternalStoragePermissionId);
            //}
        }
    }
}