import { TwitterUser } from "../../../entities";

import axios from "axios";


export class TwitterSearcher {


    public static async searchTweetsByHashtag(hashtag: string): Promise<string[] | any> {
        try {
            let param = new URLSearchParams();
            param.append("tweet.fields", "author_id");
            param.append("tweet.fields", "source");
            param.append("tweet.fields", "entities");
            param.append("tweet.fields", "attachments");
            param.append("user.fields", "username");

            const response = await axios.get("https://api.twitter.com/2/tweets/search/recent", {
                headers: {
                    "Authorization": "Bearer AAAAAAAAAAAAAAAAAAAAAA5FlQEAAAAAt69DQ0MZs8hJcH%2Bj5VjERXDM0os%3Di41A7fnlJw1vhEywNBmguB5BuoU19VgQkcaW1gGZlA1Z8JlPqO "
                },
                params: {
                    query: `#${hashtag}`,
                    max_results: 100,
                    expansions: "author_id",
                    "user.fields": "username"
                },
                paramsSerializer: {
                    indexes: null // by default: false
                }
            });

            const users: TwitterUser[] = response.data.includes.users;
            const usernames = users.map((user: { username: string; }) => user.username);
            return usernames;
        } catch (error) {
            console.error(error);
        }
    }

    public static async addPoints(users: string[], points: number, eventId: number, token:string): Promise<void> {
        try {
            let data = JSON.stringify(users);
            let config = {
                method: "post",
                url: `http://localhost:5172/api/Twitter/${eventId}/${points}`,
                headers: {
                    "accept": "*/*",
                    "Content-Type": "application/json-patch+json",
                    Authorization: `Bearer ${token}`
                },
                data: data,
                
            };
            const response = await axios.post(`http://localhost:5172/api/Twitter/${eventId}/${points}`, data, {
                headers:{
                "accept": "*/*",
                "Content-Type": "application/json-patch+json",
                'Authorization': `Bearer ${token}`
            },
            });
            // const response = 
            if (response.status != 200) throw new Error();
        }
        catch (err) {
            console.log(err);
            throw err;
        }
    }




}