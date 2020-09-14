import { createApp } from 'vue'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faFontAwesome } from '@fortawesome/free-brands-svg-icons'

library.add(faFontAwesome)
import App from './App.vue'

import router from './router'
import store from './store/index'

createApp(App).use(store).use(router).mount('#app')
