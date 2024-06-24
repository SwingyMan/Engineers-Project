import { TopNavBar } from "./TopNavBar/TopNavBar"
import { Login } from "./components/Login/Login"
import { MessageBox } from "./components/MessageBox/MessageBox"
import { Post } from "./components/MessageBox/PostClass"
var now :number
function App() {
  let post = new Post(
    "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Consectetur voluptate nostrum dolores quidem distinctio placeat, laboriosam, sed fugit eum expedita, sapiente repellendus enim. Maxime iure possimus repellendus tempora eum recusandae!",
    "John Doe",
    "https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Classes",
    Date.now(),
    "src/assets/john-doe.jpg",
     2
  )
  post.commentCount=3
  now = Date.now()
  return (
    <>
    <TopNavBar/>
     <Login/>
     <MessageBox postInfo={new Post("a","a","a",1,"src/assets/john-doe.jpg",1)}/>
     <MessageBox postInfo={post}/> 
    </>
  )
}
export function TimeElapsed(timestamp:number):string {
      let diff       
      let elapsedS =~~((now -timestamp)/1000);
      if(elapsedS>=60){

        
        let elapsedM = ~~(elapsedS)/60;

          if(elapsedM>=60){

            let elapsedH = ~~(elapsedM)/60; 

            if(elapsedH>=24){

              let elapsedD = elapsedH/24
              
              if(elapsedD>=7){
                diff = "not implemented yet"
              }
              else{
                diff=`${elapsedD} dni temu`
              }
            }
            else{
              diff = `${elapsedH} godz. temu`
            }
          }
          else{
            diff= `${elapsedM} min. temu`
          }
      }
      else{
        diff="teraz"
      }
      
  console.log()

  
    return diff
}
export default App
