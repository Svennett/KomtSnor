
var fireBaseRef;

$(document).ready(function () {
    fireBaseRef = new Firebase("https://project-1-1.firebaseio.com/");
});

function fireBaseLogin(email, password) {

    fireBaseRef.authWithPassword({
        "email": email,
        "password": password
    }, function (error, authData) {
        if (error) {
            console.log("NOOOO, SOMETHING NOT BUËNO");
        } else {
            console.log("succesfull login");
        }
    });
}