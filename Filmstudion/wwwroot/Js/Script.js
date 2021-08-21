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
       

        for(let i=0; i<movieCo.result.length; i++)
        {
            if(user == movieCo.result[i].name && pass == movieCo.result[i].password)
            {
                console.log("Det fungerar")
                main.innerHTML= ""
                showMovies(movieCo.result[i].movieCoid);
                
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

function showMyMovies(Id)
{
    fetch("../api/movieCo")
    .then(Response => Response.json())
    .then(data => {
        
        if(data.result[Id].myMovies.length != 0)
            {
        for(i = 0; data.result[Id].myMovies.length > i; i++)
        {
            let myMovies = data.result[i].myMovies
            main.insertAdjacentHTML("afterbegin", "<div id='"+myMovies.movieId +"'>" + myMovies.name  + "</br>" + myMovies.director  +"</div>" + "</br>")
        }
    }
            else
            {
                main.insertAdjacentHTML("afterbegin", "Du har tyvärr inga filmer lånade!")
            }
            main.insertAdjacentHTML("afterbegin", "<h1 id=back> Gå tillbaka</div>")
            let back = document.getElementById("back")
            back.addEventListener("click", function(){
                main.innerHTML = "";
                showMovies(Id);
            })
    })
}

function showMovies (Id)
{
    let logout = document.createElement("h1")
    logout.innerHTML = "Logga ut"
    main.appendChild(logout)
    logout.addEventListener("click", function(){
        location.reload();
    })
    
fetch("../api/movie")
    .then(Response => Response.json())
    .then(data => {
        
        main.insertAdjacentHTML("afterbegin", "<h1 id=myMovies> Mina filmer </div>")
        let movies = document.getElementById("myMovies")
        movies.addEventListener("click", function(){
            main.innerHTML = ""
            showMyMovies(Id);
        })
        for (i = 0; data.result.length > i; i++) 
        {
            let button = document.createElement("button")
            button.setAttribute("id", data.result[i].movieId)
            button.innerHTML = "Låna";
            main.insertAdjacentHTML("afterbegin", "<div id='"+data.result[i].movieId +"'>" + data.result[i].name  + "</br>" + data.result[i].director + "</br>" + data.result[i].loanable +"</div>" + "</br>")
            main.insertAdjacentElement("afterbegin", button)
           
            button.addEventListener("click", function(event){
                event.preventDefault()
                let movieid = parseInt(event.target.id)
                
                let movie = {movieId : movieid, movieCoId : Id}
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