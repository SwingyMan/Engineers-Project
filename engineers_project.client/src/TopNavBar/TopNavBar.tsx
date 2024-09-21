import styles from "../TopNavBar/TopNavBar.module.css"

export function TopNavBar(){
    return<>
        <div id={styles.topNavBar}>
            <div id={styles.Logo}>
                <img height={40} width={40} src="src/assets/logo-politechnika.png"/>
            </div>
            <div className="input-group mb-3" style={{width: '20%', height: '50px', margin: '0 auto', top: '10%'}}>
                <input type="text" style={{backgroundColor: 'white', height: '40px', marginRight: '3px'}}
                       className="form-control" placeholder="Recipient's username"
                       aria-label="Recipient's username" aria-describedby="basic-addon2"/>
                <div className="input-group-append" style={{height: '40px'}}>
                    <button className="btn btn-secondary" type="button">Szukaj</button>
                </div>
            </div>

            <button className="btn btn-secondary" type="button" style={{textAlign: 'right', height: '40px'}}>Wyloguj
            </button>
        </div>


    </>
}