export function Button({value,onClick}:{value:string,onClick:Function}) {
  return <button className="btn btn-dark" onClick={()=>onClick()}>{value}</button>;
}
