
import { TimeElapsed } from "../../App"
import { Comments } from "../Comments/Comments"
import { ImageDiv } from "../Utility/ImageDiv"
import styles from "../MessageBox/MessageBox.module.css"

let date :number = Date.now()

export function MessageBox(){
    
    return<>
    <div className={styles.MessageBox}>
        <div className={styles.header}>     
            <ImageDiv width={40} url="src/assets/john-doe.jpg"/>
            <div>
            <div>Lorem ipsum</div>
            <div className={styles.date}>{TimeElapsed(date-900000)}</div>
            <div className={styles.control}>

            </div>
            </div>
        </div>
        <hr/>
        <div>
            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Molestiae doloribus fugiat eligendi est, ex tempora nobis recusandae a eius amet quaerat soluta corrupti non iusto rerum ut iste alias ad.
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Repudiandae repellat accusantium minus, suscipit excepturi sed quos veniam impedit quibusdam inventore fugiat repellendus soluta doloremque possimus optio, nobis doloribus dolor sint!
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Ullam, dolorum earum corrupti quia corporis totam, illum veniam, suscipit ad nesciunt aliquid quae. Expedita provident odit nemo! Quam reprehenderit animi dolor.
        </div>
        <hr/>
        <Comments/>


    </div>
    
    
    </>
}