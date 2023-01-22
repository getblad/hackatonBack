import { Response } from "express"
import { systemError } from "../entities";
import { AppError } from "../enum";


export class ResponseHelper {
    static handleError(res: Response, error: systemError, isAuthentication: boolean = false): Response<any, Record<string, any>> {
        switch (error.key) {
            case AppError.ConnectionError:
                return res.status(408).json({
                    errorMessage: error.message
                });
            case AppError.NaNError:
            case AppError.NoData:
                if (isAuthentication) {
                    return res.sendStatus(403)
                }
                else {
                    return res.status(404).json({
                        errorMessage: error.message
                    });

                }
            default:
                return res.status(400).json({
                    errorMessage: error.message
                });
        }
    }
}