namespace ByteDev.Json.SystemTextJson.Serialization
{
    /// <summary>
    /// Unix epoch time precision.
    /// </summary>
    public enum UnixEpochTimePrecision
    {
        /// <summary>
        /// Epoch time number represents seconds that have elapsed since 1970-01-01T00:00:00Z (UTC).
        /// </summary>
        Seconds = 0,

        /// <summary>
        /// Epoch time number represents milliseconds that have elapsed since 1970-01-01T00:00:00Z (UTC).
        /// </summary>
        Milliseconds = 1
    }
}