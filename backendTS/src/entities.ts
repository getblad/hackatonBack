import { AppError} from "./enum";
import { Request } from "express";

export interface systemError {
    key:AppError;
    code: number;
    message: string;
}

export interface TwitterUser{
id: number;
    name:string;
    username: string;
} 


export interface environment {
    dbConnectionString: string;
    tokenSecret: string;
    logsFolder: string;
    serverPort: number;
}