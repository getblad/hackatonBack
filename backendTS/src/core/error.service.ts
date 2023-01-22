import { Dictionary } from "underscore";
import { systemError } from "../entities";
import { AppError } from "../enum";




interface IErrorService{
    getErrors(key:AppError): systemError;
    
}

class ErrorService implements IErrorService{
    private _error:Dictionary<systemError> = {};
    
    constructor() {
        this.initializeError();
    }

    public getErrors(key: AppError): systemError {
        return this._error[key]
    }
    
    // public createError(code: number, message: string): systemError {
        // const error: systemError = {
    //         code:code,
    //         message:message
    //     }
    //     return error;
    // }
    private initializeError(){
    
        this._error[AppError.General] = {
            key: AppError.General,
            code: 99,
            message:"General error"
        };
        this._error[AppError.ConnectionError] = {
            key: AppError.ConnectionError,
            code:100,
            message: "DB server connection error"
        }
        this._error[AppError.SqlQueryError] = {
            key: AppError.SqlQueryError,
            code: 101,
            message:"Incorrect query"
        }
        this._error[AppError.NaNError] = {
            key: AppError.NaNError,
            code:102,
            message: "Not a number is provided"
        }
        this._error[AppError.EmptyNumberError] = {
            key: AppError.EmptyNumberError,
            code:103,
            message: "Ther number provided is NULL"
        }
        this._error[AppError.NoData] = {
            key: AppError.NoData,
            code:104,
            message: "Empty Result"
        }
        this._error[AppError.DeletionConflict] = {
            key: AppError.DeletionConflict,
            code:105,
            message: "Delete failed due to conflict"
        }
        this._error[AppError.LoginExists] = {
            key: AppError.LoginExists,
            code:106,
            message: "This login aready exists"
        }
        this._error[AppError.IncorrectPassword] = {
            key: AppError.IncorrectPassword,
            code:106,
            message: "Entered password is not correct"
        }
    }
    
}

export default new ErrorService()