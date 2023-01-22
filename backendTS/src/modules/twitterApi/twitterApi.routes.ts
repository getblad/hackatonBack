import  { Application } from "express";
import { RouteConfig } from "../../framework/router.config";
import controller from "./twitterApi.controller";

export class TwitterRoutes extends RouteConfig {
    constructor(app: Application) {
    super(app, "TwitterRoutes")
    }
     configureRoutes(){
        this.app.route("/tweets/:hashtag").get( controller.getTweetsAmount);
        return this.app;
}
}
// router.get("/amount/:hashtag", controller.getTweetsAmount);
// router.post("/post", controller.sendMessage);
