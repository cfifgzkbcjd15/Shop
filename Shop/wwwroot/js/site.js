var page = 0;
$(function () {
    
    // Шапка навигаций
    let [ulNavbarNavs, inputSearch, aSearchIcon, aCloseIcon, ulSections] =
        [$("#ulNavbarNav"), $("#inputSearch"), $("#aSearchIcon"), $("#aCloseIcon"), $("#ulSections")];



    let namesMenu = {
        Name: ["Поставщики", "Закупки", "Товары", "Покупатели"],
        Link: ["Поставщики", "Закупки", "Товары", "Покупатели"]
    };

    for (let i = 0; i < namesMenu.Name.length; i++) {
        ulNavbarNavs.append(`
            <li class="nav-item">
                <a class="nav-link" style="text-decoration: none;" aria-current="page" href="${namesMenu.Link[i]}">${namesMenu.Name[i]}</a>
            </li>
        `);
    };

    /*let sections = {
        Name :["Одежда, обувь, аксессуары", "Продукты питания, напитки", "Строительство и ремонт",
            "Сырье и материалы", "Товары для детей, игрушки", "Дом, дача, сад", "Подарки, сувениры",
            "Электроника, бытовая техника", "Мебель", "Косметика, гигиена"],
        Link :[],
    };

    for(let i = 0; i < sections.Name.length; i++)
    {
        ulSections.append(`
        <li class="ficon"><span class="ficon-t-shirt ficon__icon ficon__icon--lg"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="url(#linear-gradient)" class="bi bi-balloon" viewBox="0 0 16 16">
        <path fill-rule="evenodd" d="M8 9.984C10.403 9.506 12 7.48 12 5a4 4 0 0 0-8 0c0 2.48 1.597 4.506 4 4.984ZM13 5c0 2.837-1.789 5.227-4.52 5.901l.244.487a.25.25 0 1 1-.448.224l-.008-.017c.008.11.02.202.037.29.054.27.161.488.419 1.003.288.578.235 1.15.076 1.629-.157.469-.422.867-.588 1.115l-.004.007a.25.25 0 1 1-.416-.278c.168-.252.4-.6.533-1.003.133-.396.163-.824-.049-1.246l-.013-.028c-.24-.48-.38-.758-.448-1.102a3.177 3.177 0 0 1-.052-.45l-.04.08a.25.25 0 1 1-.447-.224l.244-.487C4.789 10.227 3 7.837 3 5a5 5 0 0 1 10 0Zm-6.938-.495a2.003 2.003 0 0 1 1.443-1.443C7.773 2.994 8 2.776 8 2.5c0-.276-.226-.504-.498-.459a3.003 3.003 0 0 0-2.46 2.461c-.046.272.182.498.458.498s.494-.227.562-.495Z"/>
      </svg></span> <a href="${sections.Link[i]}" class="ficon__text ficon__text--lg">${sections.Name[i]}</a></li>
        `);
    };*/

    let [inputId, inputValue, inputBottom] = [$("#searchInputMain"), "", $("#inputBottom")];
    let inputValueMas = '';

        inputId.keyup(function () {
            inputValue = inputId.val();

            console.log(inputValueMas)

            let link = `https://localhost:7262/api/Products?text=${inputValue}&page=${page}`;

            if (inputValue != '' ) { inputBottom.show(); } else { inputBottom.hide(); };
            getHint(inputValue, link);
        })

    $("body").on('click', function () {
        inputBottom.hide();
    });
});



function sendMessage(hite = false) {
    inputValue = document.querySelector("#searchInputMain").value;
    var link = `https://localhost:7262/api/Products?text=${inputValue}&page=${page}`
    if (hite)
        link += "&hite=true";
    $.ajax({
        url: link,
        method: 'get',
        headers: { 'Access-Control-Allow-Origin': 'https://localhost:7262' },
        dataType: 'json',
        success: function (data) {
            addCardMain(data);
            
        }
    });
};
function getHint(text,link){
    $.ajax({
        url: `https://localhost:7262/api/Products?text=${text}`,
        method: 'get',
        headers: { 'Access-Control-Allow-Origin': 'https://localhost:7262' },
        dataType: 'json',
        success: function (data) {
            addHint(data);
        }
    });
    
}
function editInputValue(event) {
    document.querySelector("#inputBottom").innerHTML = '';
    document.querySelector("#searchInputMain").value = event.srcElement.innerText;
    let inputValueMas = document.querySelector("#searchInputMain").value;
    let link = `https://localhost:7262/api/Products?text=${inputValueMas}&page=${page}`;
    sendMessage(link);
}
function addHint(data) {
    /*console.log(data)*/
    document.querySelector("#inputBottom").innerHTML = '';
    if (data != null && data.length > 0)
        data.forEach(x => {
            if (x.name != document.querySelector("#searchInputMain").value)
                $("#inputBottom").append(`<p onclick="editInputValue(event)" class="mb-1">${x.name}</p>`)
        })

}



function addCardMain(data) {
    console.log(`${data}`)
    let [rowAddCard, bgGrey, bgDarkGrey] = [$("#rowAddCard"), '#282828', '#686868'];
    rowAddCard.empty();
    for (let x in data) {
        rowAddCard.append(`
            <div class="card-${data[x].id} me-2 mb-2" style="width: 15rem; background: ${x % 2 === 0 ? bgGrey : bgDarkGrey}; color: white;">
              <div class="card-body">
                <h5 class="card-title">${data[x].name}</h5>
              </div>
            </div>
        `);
    }
}