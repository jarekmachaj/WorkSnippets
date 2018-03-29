function findLabelsinContainer(containerClass){
    var container = document.getElementsByClassName(containerClass);
    var allLabels = [];
    for (var idx in container){
        var labelsInsideContainer = undefined;
        if (container[idx].getElementsByTagName) labelsInsideContainer = container[idx].getElementsByTagName('label');        
        if (labelsInsideContainer && labelsInsideContainer[0]) allLabels.push(labelsInsideContainer[0]);        
    }
    return allLabels;
}

function hideLabelsForInput(containerClass){
    var labels = findLabelsinContainer(containerClass);
    for (label in labels){
        if(labels[label] && labels[label].htmlFor) labels[label].style.display = 'none';
    }
}