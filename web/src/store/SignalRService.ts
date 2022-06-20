import * as SignalR from "@microsoft/signalr";

/**
 * Class which encapsulates the SignalR service
 */
class SignalRService {
  private _connection: SignalR.HubConnection;
  public connectionId: string;

  /**
   * Starts the connection to the SignalR service.  This is invoked from App.tsx after the authentication has finished
   * and we have a JWT token for the user since we need the token to negotiate the connection to SignalR.
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

    await this._connection.start();

    // This ID is needed when joining groups.
    console.log(`Connection ID: ${this._connection.connectionId}`);
    this.connectionId = this._connection.connectionId ?? "";
  }
}

export const signalRService = new SignalRService();
