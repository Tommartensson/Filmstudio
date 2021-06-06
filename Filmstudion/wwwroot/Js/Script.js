fetch("../api/movies")
    .then(Response => Response.json())
    .then(Data =>
        {
            console.log(Data)
        for(i = 0; i < Data.length; i++)
        {
            
            var newDiv = document.createElement("div")
            div.insertAdjacentElement("afterbegin", newDiv)
            newDiv.innerHTML = ' <div class="col-sm-2">'
            '<div class="card">'
                '<div class="card-header">' +
                  Data.name +
                '</div>'
                '<div class="card-body">'
                  '<h5 class="card-title">' +
                  Data.director +
                  '</h5>'
                  '<p class="card-text">' +
                  Data.releaseYear +
                  '</p>'
                  '<a href="#" class="btn btn-primary">Hyr</a>'
                '</div>'
              '</div>'
              '</div>'
        }
    })