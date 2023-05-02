const curl = "https://localhost:5001/"
let token;
let currentTest;
let currentTestId
let textIdField;
let answers = {};
let result = []

$(document).ready(function(){
	textIdField = $('#text')
	$(":button[name='loadButt']").click(() => getTest());
	$(":button[name='calcButt']").click(calcResult);
})

let addNewField = () =>{
	$('.test_container').append('<div class="test_field"> <h2>Description</h2><p>PossibleAnswer</p></div>')
}

let getId = () => {
	currentTestId = textIdField.val()
}

let updateTestPage = () => {
	$('.test_container').text('')
	let head = `<h1>Name: ${currentTest.name}</h1>`
	let description = `<p>Description: ${currentTest.description}</p>`
	let tests = ''
	currentTest.questionsList.forEach( function(element, index) {
		let temp = ``
		element.possibleAnswers.forEach( function(e, i) {
			temp += `<input type="radio" name="${index}" value="${e}">${e}`
		});
		tests += `<h3>${index} question</h3><div>${temp}</div>`
	});
	tests = `<form>${tests}</form>`
	$('.test_container').append(head + description + tests);
}

let getTest = () =>{
	getId()
	$.ajax({
	    type: "get", 
	    url: curl + `api/tests/${currentTestId}`,
	    crossDomain: true,
	    headers: {
	        "accept": "application/json",
	        "Access-Control-Allow-Origin":"*"
	    },
	    beforeSend: function(request) {
		    request.setRequestHeader("Access-Control-Allow-Origin", "*");
		}, 
	    success: function (response) {
	        currentTest = response;
	        updateTestPage();
	    },
	    data:{},
	    error: function (response) {
	        console.log("error")
		}
	});
}

let calcResult = () => {
	answers = []
	$("input[type='radio']:checked").each(function(index, el) {
		if(answers[el.name] == undefined)
			answers[el.name] = []
		answers[el.name].push(el.value)
	});
	let n = currentTest.questionsList.length
	result = []
	for (var i = 0; i < n; i++) {
		if(answers[i] == undefined)
			result.push([])
		else
			result.push(answers[i])
	}
	$.ajax({
	    type: "POST",
	    url: curl + `api/tests/result/${currentTest.id}`,
	    contentType: "application/json; charset=utf-8",
	    data: JSON.stringify(result),
	    dataType: "json",
	    success: function(response) {
	        console.log(response)
	    }
	});
}

/*let dt = JSON.stringify(
		{
    		"name": "string",
    		"description": "string",
    		"linkedCoursesIds": [
    			0
    		],
    		"questionsList": 
    		[
				{
					"type": 0,
					"possibleAnswers": [
					"string"
					],
				    "rightAnswers": [
				    	"string"
					],
				"contentType": 0
				}
	    	]
	    }
	)
	$.ajax({
	    type: "POST",
	    url: curl + "api/tests",
	    contentType: "application/json; charset=utf-8",
	    data: dt,
	    dataType: "json",
	    success: function(response) {
	        var person = response.d;
	        alert(person.Name);
	    }
	});*/