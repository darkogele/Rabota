
var processDefinitions =
[
    {
        ProcessDefinitionDto: {
            Code: "Code1", Id: 1, Name: "Nabava", States: [
                { Id: 1, Name: "Initialized", StepCol: 1},
                { Id: 2, Name: "Started", StepCol: 2 },
                { Id: 3, Name: "Product validation", StepCol: 3},
                { Id: 4, Name: "Customer validation", StepCol: 4 },
                { Id: 6, Name: "Aletrnate validation", StepCol: 4 },
                { Id: 5, Name: "Ended", StepCol: 5}
            ]
        }
    }
]
    ;

Kinetic.StepShape = function (config) {
    this._initStepShape(config);
};

Kinetic.StepShape.prototype = {
    _initStepShape: function (config) {
        Kinetic.Rect.call(this, config);
    },
    myFunc: function () {
    }
};

Kinetic.Util.extend(Kinetic.StepShape, Kinetic.Rect);


function  setStage() {
    

    var stage = new Kinetic.Stage({
        container: 'processGraph-container',
        width: 800,
        height: 600,
    });

    var layer = new Kinetic.Layer();

    window.thingGroup = new Kinetic.Group({
        x: 140,
        y: 100,
        draggable: true
    });

    layer.add(thingGroup);
    stage.add(layer);

    var processState = new Kinetic.StepShape({
        x: 50,
        y: 50,
        width: 150,
        height: 150,
        stroke: "#000",
        fill: '#ccc#',
        strokeWidth: 0,
        draggable: false,
        name: 'processState'
    });

    layer.add(processState);

    stage.draw();


}
