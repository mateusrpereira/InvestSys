namespace Application
{
    public enum ErrorCodes
    {
        // Users related codes 1 to 99
        NOT_FOUND = 1,
        COULD_NOT_STORE_DATA = 2,
        MISSING_REQUIRED_INFORMATION = 3,
        INVALID_EMAIL = 4,
        USER_NOT_FOUND = 5,
        USER_UPDATE_FAILED = 6,
        USER_DELETE_FAILED = 7,

        // Portfolio related codes 100 199
        PORTFOLIO_NOT_FOUND = 100,
        PORTFOLIO_COULD_NOT_STORE_DATA = 101,
        PORTFOLIO_MISSING_REQUIRED_INFORMATION = 102,
        PORTFOLIO_UPDATE_FAILED = 103,
        PORTFOLIO_DELETE_FAILED = 104,

        // Active related codes 200 299
        ACTIVE_NOT_FOUND = 200,
        ACTIVE_COULD_NOT_STORE_DATA = 201,
        ACTIVE_INVALID_CODE = 202,
        ACTIVE_MISSING_REQUIRED_INFORMATION = 203,
        ACTIVE_INVALID_TYPE = 204,
        ACTIVE_UPDATE_FAILED = 205,
        ACTIVE_DELETE_FAILED = 206,

        // Transaction related codes 300 399
        TRANSACTION_NOT_FOUND = 300,
        TRANSACTION_COULD_NOT_STORE_DATA = 301,
        TRANSACTION_MISSING_REQUIRED_INFORMATION = 302,
        TRANSACTION_INVALID_TYPE = 303,
        TRANSACTION_UPDATE_FAILED = 304,
        TRANSACTION_DELETE_FAILED = 305,

    }
    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}
