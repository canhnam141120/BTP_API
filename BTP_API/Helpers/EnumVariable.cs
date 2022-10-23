namespace BTP_API.Helpers
{
    public class EnumVariable
    {
        public enum StorageStatus { Waiting, Received, Sent, Recall, Refund };
        public enum Status { Waiting, Trading, Complete, Cancel };
        public enum StatusRequest { Waiting, Approved, Denied, Cancel };
        public enum Fees { S1, S13, S35, S5, B1, BM };

        public enum Message
        {
            LIST_EMPTY,
            NOT_EXIST,
            EXIST,
            ID_NOT_EXIST,
            BOOK_NOT_EXIST,
            POST_NOT_EXIST,
            COMMENT_NOT_EXIST,
            CATEGORY_NOT_EXIST,
            USER_NOT_EXIST,
            FEE_NOT_EXIST,
            REQUEST_NOT_EXIST,
            GET_SUCCESS,
            GET_FAILED,
            SEARCH_FAILED,
            CREATE_SUCCESS,
            CREATE_FAILED,
            UPDATE_SUCCESS,
            UPDATE_FAILED,
            DELETE_SUCCESS,
            DELETE_FAILED,
            REQUEST_SUCCESS,
            REQUEST_FAILED,
            ADD_SUCCESS,
            ADD_FAILED,
            NOT_YET_LOGIN,
            SUCCESS,
            FAILED, 
            ERROR, 
            EXCHANGE_NOT_EXIST,
            EXCHANGE_DETAIL_NOT_EXIST,
            RENT_NOT_EXIST,
            RENT_DETAIL_NOT_EXIST,
            BILL_NOT_EXIST,
            APPROVED,
            DENIED,
            OLD_PASSWORD_INCORRECT,
            PASSWORD_INCORRECT,
            CODE_INCORRECT,
            CHANGE_PASSWORD_SUCCESS,
            CHANGE_PASSWORD_FAILED,
            ACCOUNT_NOT_EXIST,
            ACCOUNT_NOT_VERIFY,
            ACCOUNT_IS_VERIFY,
            VERIFY_SUCCESS,
            VERIFY_FAILED,
            ACCOUNT_IS_BAN,
            LOGIN_FAILED,
            LOGIN_SUCCESS,
            LOGOUT_SUCCESS,
            LOGOUT_FAILED,
            REGISTER_SUCCESS,
            REGISTER_FAILED,
            EMAIL_IS_EXIST,
            EMAIL_NOT_EXIST,
            INVALID_TOKEN,
            ACCESS_TOKEN_NOT_YET_EXPIRED,
            REFRESH_TOKEN_NOT_EXIST,
            REFRESH_TOKEN_IS_USED,
            REFRESH_TOKEN_IS_REVOKED,
            TOKEN_NOT_MATCH,
            CODE_NOT_MATCH
        }

    }
}
