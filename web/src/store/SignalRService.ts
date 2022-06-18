import * as SignalR from "@microsoft/signalr";

/**
 * Class which encapsulates the SignalR service
 */
class SignalRService {
  private _connection: SignalR.HubConnection;

  /**
   * Starts the connection to the SignalR service.
   * @param idToken The authorization token to attach.
   */
  public async connect(idToken: string) {
    this._connection = new SignalR.HubConnectionBuilder()
      .withUrl(import.meta.env.VITE_SIGNALR_ENDPOINT as string, {
        accessTokenFactory: () => idToken,
      })
      .configureLogging(SignalR.LogLevel.Trace)
      .withAutomaticReconnect()
      .build();

    console.log("Connected SignalR");

    // This responds to the "Send" event from the server.
    this._connection.on("Send", (msg: string) => {
      console.log(`Received: ${msg}`);
    });

    this._connection.start();
  }
}

export const signalRService = new SignalRService();
