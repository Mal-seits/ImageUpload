$(() => {
    
    let id = $("#image-id").val();
    $("#like-button").on('click', function () {

      
        $.post(`/home/AddLike?id=${id}`, function () {
            $("#like-button").prop('disabled', true);
        })
    })
    setInterval(() => {
     
        $.get(`/home/GetLikes?id=${id}`, function (likes) {
            
            $("#likes-count").text(likes);
        })
    }, 1000)
})