/*
$(":input[name='nameField']").val(),
$(":input[name='descriptionField']").val()
*/
let types = []
let i = 0
const curl = "https://localhost:5001/"

$(document).ready(function(){
	$(":button[name='addButt']").click(addQuestion);
	$(":button[name='createTestButt']").click(createTest);
})

let addQuestion = () =>{
	$('.question_container').append(`
		<div class="q${i}">
			<p>Question text</p>
			<input type="text" value="" name="questionText">
			<br>
			<select>
				<option value="Single">Single</option>
				<option value="Many">Many</option>
				<option value="Free">Free</option>
			</select>
			<input type="button" value="Add answer" name="answerAddButt">
			<div class="question_list">
				
			</div>
		</div>`
	)

	let j = $(`.q${i} .question_list`).children().length / 2
	
	//let answerType = ''
	let selectorString = `div.q${i} > div.question_list`
	/*$(`.q${i} select`).change(function() {
	    answerType = $(this).val()
	    $(this).parent().attr('class');
	    $(selectorString + " > :radio").type = "s"
	});

	let inputType = 'radio'
	if(answerType == "Single")
		inputType = "radio"
	else if (answerType == "Many")
		inputType = "checkbox"*/

	let newAnswer = `
		<input type="text" name="answer${i}_${j}">
		<input type="radio" name="submitAnswer${i}">`
	$(`.q${i} :button[name=answerAddButt]`).click(() => {
		$(selectorString).append(newAnswer)
	})
	i++
}

let createTest = () => {
	/*
	{
	  "name": "string",
	  "description": "string",
	  "linkedCoursesIds": [
	    0
	  ],
	  "questionsList": [
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
	*/
	let questions = []
	$(".question_container").children().each( function(index) {
		let posAns = []
		let rightAnswersIndexes = []
		$(this).find(".question_list input[type='radio']").each(function(i) {
			if($(this).is(":checked"))
				rightAnswersIndexes.push(i)
		})
		$(this).find(".question_list input[type='text']").each(function(ind) {
			posAns.push($(this).val())
		})
		console.log(rightAnswersIndexes, posAns)
		questions.push({
			type: 0,
			possibleAnswers: posAns,
			rightAnswers: [posAns[rightAnswersIndexes[0]]],
			contentType: 0
		})
	});
	let test = {
		name: $("input[name=nameField]").val(),
		description: $("input[name=descriptionField]").val(),
		linkedCoursesIds: [0],
		questionsList: questions
	}
	$.ajax({
	    type: "POST",
	    url: curl + `api/tests`,
	    contentType: "application/json; charset=utf-8",
	    data: JSON.stringify(test),
	    dataType: "json",
	    success: function(response) {
	        console.log(response)
	    }
	});
}