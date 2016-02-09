$.blockUI.defaults = {
    message: '<img src="/Content/Images/loading.gif" />',
    css:
   {
       padding: 0,
       margin: 0,
       width: '30%',
       top: '40%',
       left: '35%',
       textAlign: 'center',
   },

    overlayCSS: {
        backgroundColor: '#000',
        opacity: 0.6,
    },

    fadeIn: 200,
    fadeOut: 400,

    showOverlay: true,
};