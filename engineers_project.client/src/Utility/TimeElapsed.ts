
export function TimeElapsed(timestamp:number):string {
    let diff       
    let elapsedS =~~((Date.now() -timestamp)/1000);
    if(elapsedS>=60){

      
      let elapsedM = ~~((elapsedS)/60);

 
        if(elapsedM>=60){ 

          let elapsedH = ~~((elapsedM)/60); 
          if(elapsedH>=24){

            let elapsedD = ~~(elapsedH/24)
            
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
  return diff
}