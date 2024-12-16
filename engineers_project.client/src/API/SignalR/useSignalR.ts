import { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import { Message } from '../DTO/Message';
import { getToken } from '../API';


interface UseSignalRProps {
  hubUrl: string;
  onMessageReceived: (message: Message) => void;
}

const useSignalR = ({ hubUrl, onMessageReceived }: UseSignalRProps) => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl,{accessTokenFactory:()=>getToken()!})
      .configureLogging(signalR.LogLevel.Information)
      .build();

    newConnection
      .start()
      .then(() => console.log('SignalR Connected'))
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
