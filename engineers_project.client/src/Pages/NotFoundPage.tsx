import { useRouteError } from "react-router";

export default function ErrorPage(){
    const error=useRouteError();
    console.log(error)
    return (<div><h1>Page not Found</h1></div>)
}