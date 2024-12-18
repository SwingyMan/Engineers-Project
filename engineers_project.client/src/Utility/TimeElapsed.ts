
export function TimeElapsed(timestamp:Date):string {
    let diff       
    let now =new Date(Date.now());
    let elapsedS =((now.getTime() -Date.parse(`${timestamp}`))/1000);

    if(elapsedS>=60){

      
      let elapsedM = ~~((elapsedS)/60);

 
        if(elapsedM>=60){ 

          let elapsedH = ~~((elapsedM)/60); 
          if(elapsedH>=24){

            let elapsedD = ~~(elapsedH/24)
            
            if(elapsedD>=7){
              diff = `${~~(elapsedD/7)} tygodni temu`
            }
            else{
              diff=`${elapsedD} dni temu`
            }
          }
          else{
            diff = `${elapsedH} godzin temu`
          }
        }
        else{
          diff= `${elapsedM} minut temu`
        }
    }
    else{
      diff="teraz"
    }
  return diff
}