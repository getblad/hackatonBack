import express, { Express, Application, Request, Response } from "express"
import * as http from "http"
import cors from "cors"
// import dotenv from "dotenv"
import { RouteConfig } from "./framework/router.config"
import { TwitterRoutes} from "./modules"
import { StaticEnvironment } from "./core/environment.static";
import LoggerService from "./core/logger.service"

const routes: Array<RouteConfig> = [];
const app: Express = express();
import { auth } from "express-oauth2-jwt-bearer"
// import  { expressjwt, GetVerificationKey }  from "express-jwt"
// import jwks from "jwks-rsa"

const checkJwt = auth({
    audience: "https://hackaton-platforms/api",
  issuerBaseURL: `https://hackaton-platforms.eu.auth0.com/`,
});


app.use(checkJwt);
app.use(express.json());
app.use(cors());

// var jwtCheck = expressjwt({
    
    
//       secret: jwks.expressJwtSecret({
//           cache: true,
//           rateLimit: true,
//           jwksRequestsPerMinute: 5,
//           jwksUri: "https://hackaton-platforms.eu.auth0.com/.well-known/jwks.json"
//     }) as GetVerificationKey,
//     audience: "https://hackaton-platforms/api",
//     issuer: "https://hackaton-platforms.eu.auth0.com/",
//     algorithms: ["RS256"]
// });

// app.use(jwtCheck);

routes.push(new TwitterRoutes(app));
// routes.push(new AuthenticationRoutes(app));

// app.use((req, res, next) => {
//   // set the CORS policy
//   res.header("Access-Control-Allow-Origin", "*");
//   // set the CORS headers
//   res.header("Access-Control-Allow-Headers", "origin, X-Requested-With,Content-Type,Accnept, Authorization");
//   // set the CORS method headers
//   if (req.method === "OPTIONS") {
//       res.header("Access-Control-Allow-Methods", "GET PATCH DELETE POST");
//       return res.status(200).json({});
//   }
//   next();
// });

// app.get("/", (req: Request, res: Response) => {
//   res.send("Welcome world");
// })
process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
app.use((req, res, next) => {
  const error = new Error("not found");
  return res.status(404).json({
      message: error.message
  });
});

const server: http.Server = http.createServer(app);
server.listen(StaticEnvironment.serverPort, () => {
  LoggerService.info(`Server is running on ${StaticEnvironment.serverPort}`);
  routes.forEach((route: RouteConfig) => {
    LoggerService.info(`Routes configured for ${route.getName()}`)
    
  })
})

