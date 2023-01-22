export enum AppError{
    General = "General",
    ConnectionError = "ConnectionError",
    SqlQueryError = "Incorrect query",
    NaNError =  "Not a number is provided",
    EmptyNumberError = "Ther number provided is NULL",
    NoData = "Empty result",
    DeletionConflict = "Delete failed due to conflict",
    LoginExists = "This login aready exists",
    IncorrectPassword = "Entered password is not correct"
}
