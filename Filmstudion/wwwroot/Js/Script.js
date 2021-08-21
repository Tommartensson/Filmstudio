let main = document.getElementById("main")
let UserName = document.getElementById("User")
let Password = document.getElementById("Pass")
let checkIf = document.getElementById("CheckIf")
let Namename = document.getElementById("name")
let pass = document.getElementById("password")
let country = document.getElementById("country")
let mail = document.getElementById("mail")
let button = document.getElementById("button")
let nameOfCo = document.getElementById("nameOfCo")
let number = document.getElementById("number")


checkIf.addEventListener("click", function(){
    fetch("../api/movieCo")
    .then(Response => Response.json())
    .then(movieCo => {
        const user = UserName.value
        const pass = Password.value
        console.log(movieCo.result)

        for(let i=0; i<movieCo.result.length; i++)
        {
            if(user == movieCo.result[i].name && pass == movieCo.result[i].password)
            {
                console.log("Det fungerar")
                main.innerHTML= ""
                showMovies(movieCo.result.movieCoid);
                break;
            }
            else
            {
                console.log("det fungerar inte")
            }
        }
    })
})

button.addEventListener("click", function() {
    let data = {name : Namename.value, 
                password: pass.value,
                place: country.value,
                mail: mail.value,
                nameOfCo: nameOfCo.value
                }
    fetch("../api/movieCo", {method: "POST",
    body: JSON.stringify(data),
    headers: {"Content-type": "application/json; charset=UTF-8"}})
    .then(Response => Response.json())
    .then(json => console.log(json))
    .catch(err => console.log(err))
    
    
})

function showMovies (MovieCoId)
{
fetch("../api/movie")
    .then(Response => Response.json())
    .then(data => {
        console.log(MovieCoId)
        for (i = 0; data.result.length > i; i++) 
        {
            console.log(data.result)
            let button = document.createElement("button")
            button.innerHTML = "Låna";
            main.insertAdjacentHTML("afterbegin", data.result[i].name + "</br>" + data.result[i].director + "</br>" + data.result[i].loanable +"</br>" + "</br>")
            main.insertAdjacentElement("afterbegin", button)
            button.addEventListener("click", function(){
                console.log(data.result.MovieId)
                let movie = {MovieId : data.result.MovieId, MovieCoId : MovieCoId}
                fetch("../api/movieCo/AddMovie", {
                    method: "POST",
                    body: JSON.stringify(movie),
                    headers: {"Content-type": "application/json; charset=UTF-8"}
                    
                })
                .then(Response => Response.json())
                .then(json => console.log(json))
                .catch(err => console.log(err))
                    
                
            })
            
        }
            
    }
    )
}