$(() => {
    function fillPage() {
        $.get("/home/GetAllImages", function (images) {
            images.forEach(i => {
                $("#body").append(`

            <div class="jumbotron" style="margin-bottom: 20px; text-align: center;">
                    <a href="/home/ViewImage?id=${i.id}">
                        <h3>${i.title}</h3>
                        <h5>Uploaded at: ${i.date}</h5>
                        <img style="width: 400px;" src="/Uploads/${i.fileName}" />
                    </a>
                
                </div>

`)
            })
        })
    }
    fillPage();
})