var Zap = {

    pre_subscribe: function (bundle) {
        "use strict";

        bundle.request.data = JSON.stringify({
            'subscription_url': bundle.subscription_url,
            'target_url': bundle.target_url,
            'event': bundle.event,
            'account_name': 'fff'
        });

        //console.log(bundle.request.auth[0]);

        return bundle.request;
    },

    post_subscribe: function (bundle) {
        "use strict";

        var subscribe_data = JSON.parse(bundle.response.content);

        return subscribe_data;
    },

    pre_unsubscribe: function (bundle) {
        "use strict";

        bundle.request.data = JSON.stringify({
            'subscription_id': bundle.subscribe_data.subscription_id,
            'subscription_url': bundle.subscription_url,
            'target_url': bundle.target_url
        });

        bundle.request.method = 'DELETE';

        return bundle.request;
    }
};