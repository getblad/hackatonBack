// const axios = require("axios");\

import { DataTypes, Model, Sequelize } from "sequelize";
class User extends Model { }



export class DbRepositories {
     _sequelize: Sequelize
    
    constructor() {
        const sequelize = new Sequelize("hp-server", "azureuser", "Vflbrf1131310", {
            host: "hpserver-1.database.windows.net",
            dialect: "mssql",
            dialectOptions: {
                encrypt: true
            },
            port: 1433,
            logging: console.log
        });
        User.init(
            {
                UserGitHub: {
                    type: DataTypes.STRING,
                    allowNull: false
                }

            }, {
                sequelize,
                modelName: "user"
            }
        );
        this._sequelize = sequelize;
    
    }
    public Checking = async () => {
        try {
        await this._sequelize.authenticate();
            console.log("Connection has been established successfully.");
        } catch (error) {
            console.error("Unable to connect to the database:", error);
        }
    
}
}