﻿# LoadingIndicatiorService
See ILoadingIndicatorService for more info on how to use. 

Subscribe:
`_loadingIndicatorService.Subscribe(actionToOccurWhenLoadingStarted, actionToOccurWhenLoadingEnds);`

Start Loading Process:
`using(_loadingIndicatorService.IsLoading())
{
  // a task that will take some time. 
}`
