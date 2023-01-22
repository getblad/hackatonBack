import * as _ from "underscore";
import errorService from "../../core/error.service";
import { AppError } from "../../enum";
import { DbRepositories } from "./helpers/dbRepository";
import { TwitterSearcher} from "./helpers/twitterSearcher";

const axios = require("axios");

interface ITwitterService {
    getTweets(): Promise<any[]>;
    // getTweetsAmount(hashtag: string): Promise<any>;
    // sendMessage(): Promise<string>;
}

export class TwitterService implements ITwitterService {

    constructor() { }

    // public async sendMessage(): Promise<string> {
        // let tweet: string = await TwitterSender.tweet();
        // return tweet;
    // }

    public async getTweets(): Promise<any[]> {
        let tweets: any[] = await TwitterSearcher.searchTweetsByHashtag("NikitaIsmagilov");
        return tweets;
    }

    public async getTweetsAmount(hashtag: string, points: number, eventId: number, token:string): Promise<any> {
        
        try {
            let usernames: string[] = await TwitterSearcher.searchTweetsByHashtag(hashtag);
            await TwitterSearcher.addPoints(usernames, points, eventId, token);
            return;
        }
        catch {
            throw errorService.getErrors(AppError.General);
        }

        // await DbRepositories.Checking()
    }

    // private parseLocalUser(local: localUser): user {
    //     return {
    //         id: local.user_id,
    //         name: local.user_name,
    //         login: local.user_login,
    //         email: local.user_email,
    //         password: local.user_password,
    //         role: local.user_role
    //     };
    // }
}