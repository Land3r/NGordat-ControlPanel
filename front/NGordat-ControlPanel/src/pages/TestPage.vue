<template>
  <app-formpopup
    title="Nouvel item"
    cancelbutton
    resetbutton
    confirmbutton
  >
    <app-groceryitemform />
  </app-formpopup>
</template>

<script>
import FormPopup from 'components/common/forms/FormPopup'
import GroceryItemForm from 'components/forms/groceries/GroceryItemForm'

export default {
  name: 'TestPage',
  components: {
    'app-formpopup': FormPopup,
    'app-groceryitemform': GroceryItemForm
  }
}
</script>
