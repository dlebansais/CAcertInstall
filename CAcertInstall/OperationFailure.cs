namespace CAcertInstall
{
    /// <summary>
    /// Specifies different types of forced failures.
    /// </summary>
    public enum OperationFailure
    {
        /// <summary>
        /// No forced failure.
        /// </summary>
        None,

        /// <summary>
        /// Failure when checking if installed.
        /// </summary>
        InstalledCheck,

        /// <summary>
        /// Failure when installing.
        /// </summary>
        Install,

        /// <summary>
        /// failure when uninstalling.
        /// </summary>
        Uninstall,
    }
}
