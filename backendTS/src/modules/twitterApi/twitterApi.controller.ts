import { Request, Response, NextFunction } from "express";
import { systemError } from "../../entities";
import { ResponseHelper } from "../../framework/response.helper";
import  ErrorService  from "../../core/error.service";
import { TwitterService } from "./twitterApi.service";
import { RequestHelper } from "../../core";
import { AppError } from "../../enum";

// const errorService: ErrorService = new ErrorService();
const twitterService: TwitterService = new TwitterService();

const getTweets = async (req: Request, res: Response, next: NextFunction) => {

    twitterService.getTweets()
        .then((result: any) => {
            return res.status(200).json(result);
        })
        .catch((error: systemError) => {
            return ResponseHelper.handleError(res, error);
        });
};


const getTweetsAmount = async (req: Request, res: Response, next: NextFunction) => {
    try {
        let hashtag: string = req.params.hashtag;
        let token:string = req.auth?.token!;
        let points: number | systemError = RequestHelper.parseNumericValue(req.body.points);
        let eventId: number | systemError = RequestHelper.parseNumericValue(req.body.eventId);
        if (typeof points === "number" && typeof eventId === "number") {
            twitterService.getTweetsAmount(hashtag, points, eventId, token)
                .then((result: any) => {
                    return res.status(200).json();
                })
                .catch((error: systemError) => {
                    return ResponseHelper.handleError(res, error);
                });
        
        }
        else {
            throw ErrorService.getErrors(AppError.EmptyNumberError)
        }
    }
    catch (e:any){
        return ResponseHelper.handleError(res, e);
    }
};

// const sendMessage = async (req: Request, res: Response, next: NextFunction) => {

//     twitterService.sendMessage()
//         .then((result: any) => {
//             return res.status(200).json(result);
//         })
//         .catch((error: systemError) => {
//             return ResponseHelper.handleError(res, error);
//         });
// }


export default { getTweets, getTweetsAmount  };