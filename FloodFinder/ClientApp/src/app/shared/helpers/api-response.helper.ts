const errorsMap = new Map([
  ['0', x=> 'Unable to reach server. Please contact your system administrator.'],
  ['400',x=> parseError(x)],
  ['401',x=> 'Unauthorized. Please contact your system administrator.'],
  ['404',x=> 'Page not found. Please contact your system administrator.'],
  ['default',x=> 'Unknown error. Please contact your system administrator.'],
  ['500',x=> 'Server error. Please contact your system administrator.']
]);

function parseError(error: any): string{
  let errorMessage = '';

  if (error.error) {
    for (const i in error.error) {
      // tslint:disable-next-line: forin
      for (const j in error.error[i]) {
        errorMessage += error.error[i][j];
      }
    }
  }

  if (error.error.message) {
    errorMessage = error.error.message;
  }

  return errorMessage;
}

export const parseErrorMessage = (error: any) => {
  const errorMessageCallback = errorsMap.get(error.status.toString()) || errorsMap.get('default');
  return errorMessageCallback(error);
}
