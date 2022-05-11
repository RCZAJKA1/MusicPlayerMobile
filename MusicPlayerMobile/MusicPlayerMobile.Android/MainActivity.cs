﻿namespace MusicPlayerMobile.Droid
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

        /// <summary>
        ///     The request stroage permission identifier.
        /// </summary>
        private const int RequestExternalStoragePermissionId = 0;

        /// <summary>
        ///     The permissions.
        /// </summary>
        private readonly string[] Permissions = { ReadExternalStoragePermission };

        /// <inheritdoc/>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            this.TryToGetPermissions();

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
                            Toast.MakeText(this, "Special permissions granted", ToastLength.Short).Show();
                        }
                        else
                        {
                            //Permission Denied :(
                            Toast.MakeText(this, "Special permissions denied", ToastLength.Short).Show();

                        }
                    }

                    break;
            }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        ///     Get permissions if the SDK is greater than or equal to version 23.
        /// </summary>
        private void TryToGetPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                this.GetPermissions();
                return;
            }
        }

        /// <summary>
        ///     Gets the permissions to access device data.
        /// </summary>
        private void GetPermissions()
        {
            if (this.CheckSelfPermission(ReadExternalStoragePermission) == (int)Permission.Granted)
            {
                Toast.MakeText(this, "Read External Storage permission granted", ToastLength.Short).Show();
                return;
            }

            if (this.ShouldShowRequestPermissionRationale(ReadExternalStoragePermission))
            {
                //set alert for executing the task
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Permissions Needed");
                alert.SetMessage("The application need special permissions to continue");
                alert.SetPositiveButton("Request Permissions", (senderAlert, args) =>
                {
                    this.RequestPermissions(this.Permissions, RequestExternalStoragePermissionId);
                });

                alert.SetNegativeButton("Cancel", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();

                return;
            }

            this.RequestPermissions(this.Permissions, RequestExternalStoragePermissionId);
        }
    }
}