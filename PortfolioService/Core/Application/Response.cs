namespace Application
{
    public enum ErrorCodes
    {
        // Users related codes 1 to 99
        NOT_FOUND = 1,
        COULD_NOT_STORE_DATA = 2,
        INVALID_PERSON_ID = 3,
        MISSING_REQUIRED_INFORMATION = 4,
        INVALID_EMAIL = 5,
        USER_NOT_FOUND = 6,
        USER_UPDATE_FAILED = 7,
        USER_DELETE_FAILED = 8,

        // Portfolio related codes 100 199
        PORTFOLIO_NOT_FOUND = 100,
        PORTFOLIO_COULD_NOT_STORE_DATA = 101,
        PORTFOLIO_INVALID_PERSON_ID = 102,
        PORTFOLIO_MISSING_REQUIRED_INFORMATION = 103,
        PORTFOLIO_INVALID_EMAIL = 104,
        PORTFOLIO_UPDATE_FAILED = 105,
        PORTFOLIO_DELETE_FAILED = 106,

        // Active related codes 200 299
        ACTIVE_NOT_FOUND = 200,
        ACTIVE_COULD_NOT_STORE_DATA = 201,
        ACTIVE_INVALID_PERSON_ID = 202,
        ACTIVE_MISSING_REQUIRED_INFORMATION = 203,
        ACTIVE_INVALID_EMAIL = 204,
        ACTIVE_UPDATE_FAILED = 205,
        ACTIVE_DELETE_FAILED = 206,

        /* 
         // Booking related codes 200 499
         BOOKING_NOT_FOUND = 200,
         BOOKING_COULD_NOT_STORE_DATA = 201,
         BOOKING_INVALID_PERSON_ID = 202,
         BOOKING_MISSING_REQUIRED_INFORMATION = 203,
         BOOKING_INVALID_EMAIL = 204,
         BOOKING_GUEST_NOT_FOUND = 205,
         BOOKING_ROOM_CANNOT_BE_BOOKED = 206,

         // Payment related codes 500 - 1500
         PAYMENT_INVALID_PAYMENT_INTENTION = 500,
         PAYMENT_PROVIDER_NOT_IMPLEMENTED = 501,*/

    }
    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}
