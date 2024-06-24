
import { TimeElapsed } from "../../App"
import { Comments } from "../Comments/Comments"
import { ImageDiv } from "../Utility/ImageDiv"
import styles from "../MessageBox/MessageBox.module.css"
import { Post } from "./PostClass"


export function MessageBox(props: { postInfo: Post}){
    
    return<>
    <div className={styles.MessageBox}>
        <div className={styles.header}>     
            <ImageDiv  width={40} url={props.postInfo.img}/>
            <div>
            <div>{props.postInfo.name}</div>
            <div className={styles.date}>{TimeElapsed(props.postInfo.dateOfCreation)}</div>
            <div className={styles.control}>

            </div>
            </div>
        </div>
        <hr/>
        <div>
           {props.postInfo.content}
        </div>
        <hr/>
        <Comments/>


    </div>
    
    
    </>
}