import { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import { Message } from '../DTO/Message';
import { getHost, getToken } from '../API';


interface UseSignalRProps {
  onMessageReceived: (message: Message) => void;
}
const hubUrl = `${getHost}/chat`
const useSignalR = ({ onMessageReceived }: UseSignalRProps) => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl,{accessTokenFactory:()=>getToken()!})
      .configureLogging(signalR.LogLevel.Information)
      .build();

    newConnection
      .start()
      .catch((err) => console.error('SignalR Connection Error: ', err));

    // Listen for the SignalR event and handle it
    newConnection.on('ReceiveMessage', (message: Message) => {
      onMessageReceived(message);
    });

    setConnection(newConnection);

    // Cleanup when component is unmounted
    return () => {
      newConnection.stop();
    };
  }, [hubUrl, onMessageReceived]);

  return connection;
};

export default useSignalR;
