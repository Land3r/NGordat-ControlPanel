import common from './common'

import mainlayout from './layouts/mainlayout'

import indexpage from './pages/indexpage'

import loginpage from './pages/user/loginpage'
import forgotpasswordpage from './pages/user/forgotpasswordpage'
import resetpasswordpage from './pages/user/resetpasswordpage'
import registerpage from './pages/user/registerpage'
import activatepage from './pages/user/activatepage'

import groceriesindexpage from './pages/groceries/indexpage'
import groceriesreferentialpage from './pages/groceries/referentialpage'

export default {
  ...common,

  mainlayout: {
    ...mainlayout
  },
  indexpage: {
    ...indexpage
  },
  loginpage: {
    ...loginpage
  },
  forgotpasswordpage: {
    ...forgotpasswordpage
  },
  resetpasswordpage: {
    ...resetpasswordpage
  },
  registerpage: {
    ...registerpage
  },
  activatepage: {
    ...activatepage
  },
  groceriesindexpage: {
    ...groceriesindexpage
  },
  groceriesreferentialpage: {
    ...groceriesreferentialpage
  }
}
