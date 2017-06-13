/**
 * Created by khoaln on 7/14/2015.
 */
var isSupportLocalStorage = Modernizr.localstorage;

function getComparisonStorage(){
    if(isSupportLocalStorage){
        strJobList =  localStorage.jobComparison;
    }else{
        strJobList= getCookie('jobComparison');
    }

    if(typeof strJobList == 'undefined' || strJobList==null) {
        jobList={};
    }
    else{
        jobList=JSON.parse(strJobList);
    }
    return jobList;

}
function setComparisonStorage(data){
    if(isSupportLocalStorage){
        localStorage.jobComparison=data;
    }else{
        return setCookie('jobComparison',data,1);
    }
}

function cleanOldComparison(jobList){
    var currentDate = new Date();
    var isPersist=false;


    for(var jobId in jobList){
        var job=jobList[jobId];
        //compare job difference today => remove it
        var expiredDate=new Date(job.job_expireddate);
        //set expired date is the end of this day
        expiredDate.setDate(expiredDate.getDate() + 1);
        if(currentDate > expiredDate){
            //delete job in list comparison
            delete jobList[jobId];
            isPersist=true;
        }
    }
    //persist job to local storage
    if(isPersist){
        setComparisonStorage(JSON.stringify(jobList));
    }
    return jobList;
}

function addJobComparison(jobId,jobName,jobAlias,jobExpiredDate,limitErrorMessage){
    var jobList=getComparisonStorage();
    //check Job List <3
    if(Object.keys(jobList).length<5){
        //check job already exist in list comparison
        if(typeof jobList[jobId] == 'undefined'){
            var ordering=Object.keys(jobList).length+1;
            var job={job_id:jobId,job_title:jobName,job_alias:jobAlias,job_expireddate: jobExpiredDate,ordering:ordering};
            jobList[jobId]=job;
            setComparisonStorage(JSON.stringify(jobList))
            return true;
        }
        else{
            //throw exist jobJd
            console.log("Job Exist");
        }
    }
    else{
        var element="[data-jobid='"+jobId+"']";
        $(element).prop('checked',false).popover({
            placement: 'right',
            content: limitErrorMessage,
            trigger: 'click',
            html: true
        });
        $(element).popover("show");
        setTimeout(function(){
            $(element).popover("hide");
            $(element).popover('destroy');
        },2500);
    }
    return false;

}

function removeJobComparison(jobId){
    var jobList=getComparisonStorage();

    //check Job List <3
    if(Object.keys(jobList).length>0){
        //check job already exist in list comparison
        if(typeof jobList[jobId] != 'undefined'){
            delete jobList[jobId];
            if(Object.keys(jobList).length>0){
                //re-ordring list job
                var jobListSorted= Object.keys(jobList).sort(function(a,b){return jobList[a].ordering > jobList[b].ordering });
                for(var i=0;i<jobListSorted.length;i++){
                    var jobId=jobListSorted[i];
                    var newJob = jobList[jobId];
                    newJob.ordering = i+1;
                    jobList[jobId]=newJob;
                }
            }
            setComparisonStorage(JSON.stringify(jobList));
            return true;
        }
    }
    else{
        //throw popup maximize 3
        console.log("Emtpy list");
    }
    return false;
}

