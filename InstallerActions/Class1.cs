using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace InstallerActions
{
    [RunInstaller(true)]
    public class CustomActions : Installer
    {
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver); // it's fine here

            try
            {
                string targetDir = Context.Parameters["targetdir"] ?? Context.Parameters["TARGETDIR"];

                if (string.IsNullOrEmpty(targetDir))
                    return;

                if (targetDir.EndsWith("\\"))
                    targetDir = targetDir.Substring(0, targetDir.Length - 1);

                string databasePath = Path.Combine(targetDir, "Database");

                if (Directory.Exists(databasePath))
                {
                    DirectorySecurity dSecurity = Directory.GetAccessControl(databasePath);
                    SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                    dSecurity.AddAccessRule(new FileSystemAccessRule(
                        everyone,
                        FileSystemRights.FullControl,
                        InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                        PropagationFlags.None,
                        AccessControlType.Allow));

                    Directory.SetAccessControl(databasePath, dSecurity);

                    File.WriteAllText(Path.Combine(targetDir, "permission_success.log"),
                        "Permissions set successfully on " + databasePath);
                }
                else
                {
                    File.WriteAllText(Path.Combine(targetDir, "permission_error.log"),
                        "Database directory not found at: " + databasePath);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    string targetDir = Context.Parameters["targetdir"] ?? Context.Parameters["TARGETDIR"];
                    if (!string.IsNullOrEmpty(targetDir))
                    {
                        File.WriteAllText(Path.Combine(targetDir, "permission_error.log"),
                            "Error setting permissions: " + ex.ToString());
                    }
                }
                catch
                {
                    // silently ignore logging errors
                }
            }
        }
    }
}
