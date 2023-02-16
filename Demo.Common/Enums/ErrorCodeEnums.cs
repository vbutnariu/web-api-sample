namespace Demo.Common.Enums
{
    public enum ErrorCodeEnum : int
    {


        UnexpectedException = -1,

        #region Security
        //Security exceptions
        //Prefix 1
        ChangePasswordAtNextLogon = 100002,
        InvalidGrant = 100003,
        #endregion

        #region Web Api
        //WEB API EXCEPTIONS 
        // prefix 2
        DuplicateEntry = 200001,
        InvalidModel = 200002,
        ItemCannotBeDeletedItemAlreadyInUse = 200003,
        NetworkConnectionUnavailable = 200009,
        OperationNotAllowed = 200010,
        #endregion


    }
}
