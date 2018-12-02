// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function(){

    /* LOGIN */ 
    var userlog = $('#email').val();
    var passlog = $('#pass').val();
    if(userlog != "" && passlog != "") {
        const urlogin = location.protocol + "//" + location.hostname + "/Login";
        let datos = {
            'user':userlog,
            'pass':passlog
        };
        $.ajax({
            url:urlogin,
            data:datos,
            success:function(response){
                if (response) {
                    //hacer algo
                }
            },
            error:function(error){
                if (error) {
                    //ver cual es el error
                }
            }
        });
    }

    /* REGISTRO */
    var nombre = $('#name').val();
    var apellido = $('#lastname').val();
    var correo = $('#email').val();
    var pass = $('#password').val();
    var terminos = $('#terms').val();
    if (nombre != "" && apellido != "" && correo != "" && pass != "" && terminos != false) {
        const urlregistro = location.protocol + "//" + location.hostname + "/Registro";
        let datos = {
            'name':nombre,
            'lastname':apellido,
            'email':correo,
            'password':pass,
            'terms':terminos
        };
        $.ajax({
            url:urlregistro,
            data:datos,
            success:function(response){
                if (response) {
                    //hacer algo
                }
            },
            error:function(error){
                if (error) {
                    //ver cual es el error
                }
            }
        });
    }

    /* PUBLICAR */

    var titulo = $('#titulo').val();
    var precio = $('precio').val();
    var descripcion = $('descripcion').val();
    var divdrop = $('.drop-files')[0];
    var div = $('.publicar-fotos')[0];
    var btnupload = $('#btn-upload')[0];
    var btnfiles = $('#files-upload')[0];
    var data = 0;

    ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
        div.addEventListener(eventName, preventDefaults, false);
    });

    div.addEventListener('drop', drop, false);
    div.addEventListener('dragover',(e)=>{
        div.style.color = "green";
    });
    div.addEventListener('dragleave',(e)=>{
        div.style.color = "orange";
    });
    btnupload.addEventListener('click',(e)=>{
        preventDefaults(e);
        btnfiles.click();
    });
    btnfiles.addEventListener('change',(e)=>{
        let file = btnfiles.files;
        mostrarArchivo(file);
    });

    function drop(e) {
        let dt = e.dataTransfer;
        let file = dt.files;
        div.style.color = "rgba(0,0,0,.3)";
        mostrarArchivo(file); 
    }

    function preventDefaults(e) {
        e.preventDefault();
        e.stopPropagation();
    }

    function mostrarArchivo(file){

        function volver(){
            divdrop.childNodes[1].innerText = "Agregar fotos";
        }

        var reader = new FileReader();
            
        if (file[0].type.startsWith('image/')) {
            if (file.length == 1) {
                let divpad = $('.publicar-fotos')[0];
                let divimg = document.createElement('div');
                divimg.setAttribute('class','img-upload');
                divdrop.innerHTML='';
                divpad.style.padding = '2% 0 2% 0';
                if (file) {
                    reader.readAsDataURL(file[0]);
                }
                reader.onloadend = function (){
                    divimg.style.backgroundImage= "url("+reader.result+")";
                    divdrop.appendChild(divimg);
                    data = {
                        'titulo':titulo,
                        'precio':precio,
                        'descripcion':descripcion,
                        'imagen':reader.result
                    };
                }
            }else {
                divdrop.childNodes[1].innerText = "Solo puede subir una imagen.";
                setTimeout(volver,5000);
            }
        } else {
            divdrop.childNodes[1].innerText = "Solo se aceptan imagenes.";
            setTimeout(volver,5000);
        }

    }

    var btnsend = $('#btn-send')[0];

    btnsend.addEventListener('click',saveProduct,false);

    function saveProduct(e){
        preventDefaults(e);
        
        let url = location.protocol + "//" + location.hostname + "/SavePhoto";
        let formData = new FormData();
        formData.append('file', data);
        fetch(url, {
          method: 'POST',
          body: formData
        })
        .then(() => {  })
        .catch(() => {  })
    }

});