import { LogLevel, HubConnectionBuilder } from "@aspnet/signalr";
import store from '../store';
import config from '../config.js';

const buildConnection = () => {
    return new HubConnectionBuilder()
        .withUrl(config.restAPI + '/postHub')
        .configureLogging(LogLevel.Information)
        .build();
}

const startAndStoreConnection = (connection) => {
    connection.start().catch(function (err) {
        return console.error(err);
    });
    store.state.connection = connection;
}

const returnPosts = () => {
    store.state
        .connection
        .invoke("ReturnAllPosts")
        .catch(function (err) {
            return console.error(err);
        });
}

export const signalRService = {
    buildConnection,
    startAndStoreConnection,
    returnPosts
};