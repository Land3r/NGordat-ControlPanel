<template>
  <q-page class="q-px-md">
    <h3>{{ $t('groceriesreferentialpage.title') }}</h3>
    <q-tabs
      v-model="activeTab"
      dense
      class="text-black"
      active-color="white"
      active-bg-color="primary"
      indicator-color="primary"
      align="justify"
    >
      <q-tab
        name="actions"
        label="Actions"
        icon="add_shopping_cart"
      />
      <q-tab
        name="items"
        label="Items"
        icon="shopping_cart"
      />
      <q-tab
        name="quantities"
        label="Quantities"
        icon="sort"
      />
      <q-tab
        name="meaninglesswords"
        label="Meaningless Words"
        icon="translate"
      />
    </q-tabs>

    <q-separator />

    <q-tab-panels
      v-model="activeTab"
      animated
    >
      <q-tab-panel name="actions">
        <div class="text-h6 q-py-sm">
          <q-icon
            name="add_shopping_cart"
            class="q-mx-sm"
          />
          Actions
        </div>
        <q-list
          bordered
          separator
        >
          <q-item
            v-for="item in actions"
            :key="item.Id"
            v-ripple
            clickable
          >
            <q-item-section avatar>
              <q-icon
                color="primary"
                :name="item.icon"
              />
            </q-item-section>
            <q-item-section>{{ item.name }}</q-item-section>
          </q-item>
        </q-list>
      </q-tab-panel>

      <q-tab-panel name="items">
        <div class="text-h6 q-py-sm">
          <q-icon
            name="shopping_cart"
            class="q-mx-sm"
          />
          Items
          <span class="float-right">
            <q-chip
              outline
              color="teal"
              text-color="white"
              icon="add_circle_outline"
              class="gt-xs"
              clickable
              @click="showNewItem"
            >
              Ajouter un item
            </q-chip>
            <q-chip
              outline
              color="teal"
              text-color="white"
              icon="add_circle_outline"
              class="lt-sm"
              clickable
              @click="showNewItem"
            />
          </span>
        </div>
        <q-list
          bordered
          separator
        >
          <q-item
            v-for="item in items"
            :key="item.Id"
            v-ripple
            clickable
          >
            <q-item-section avatar>
              <q-icon
                color="primary"
                :name="item.icon"
              />
            </q-item-section>
            <q-item-section>{{ item.name }}</q-item-section>
            <q-item-section side>
              <q-item-label caption>
                aka: {{ item.aliases }}
              </q-item-label>
            </q-item-section>
          </q-item>
        </q-list>
      </q-tab-panel>

      <q-tab-panel name="quantities">
        <div class="text-h6 q-py-sm">
          <q-icon
            name="sort"
            class="q-mx-sm"
          />
          Quantities
        </div>
        <q-list
          bordered
          separator
        >
          <q-item
            v-for="item in quantities"
            :key="item.Id"
            v-ripple
            clickable
          >
            <q-item-section>{{ item.name }}</q-item-section>
            <q-item-section side>
              <q-item-label caption>
                Value: {{ item.value }}
              </q-item-label>
            </q-item-section>
          </q-item>
        </q-list>
      </q-tab-panel>

      <q-tab-panel name="meaninglesswords">
        <div class="text-h6 q-py-sm">
          <q-icon
            name="translate"
            class="q-mx-sm"
          />
          Meaningless Words
        </div>
        <q-list
          bordered
          separator
        >
          <q-item
            v-for="item in meaninglesswords"
            :key="item.Id"
            v-ripple
            clickable
          >
            <q-item-section>{{ item.name }}</q-item-section>
          </q-item>
        </q-list>
      </q-tab-panel>
    </q-tab-panels>
  </q-page>
</template>

<script>
// import xss from 'xss'
// import { NotifySuccess, NotifyFailure } from 'data/notify'

import GroceryActionService from 'services/GroceryActionService'
import GroceryItemService from 'services/GroceryItemService'
import GroceryQuantityService from 'services/GroceryQuantityService'
import GroceryMeaninglessWordService from 'services/GroceryMeaninglessWordService'

export default {
  name: 'GroceriesReferentialPage',
  components: {
  },
  data: function () {
    return {
      activeTab: 'actions',
      actions: [],
      items: [],
      quantities: [],
      meaninglesswords: []
    }
  },
  created: function () {
    const groceryActionService = new GroceryActionService()
    groceryActionService.doGet().then((response) => {
      this.actions = response
    }).catch((response) => {
      console.log('Error while getting grocery actions.')
    })

    const groceryItemService = new GroceryItemService()
    groceryItemService.doGet().then((response) => {
      this.items = response
    }).catch((response) => {
      console.log('Error while getting grocery items.')
    })

    const groceryQuantityService = new GroceryQuantityService()
    groceryQuantityService.doGet().then((response) => {
      this.quantities = response
    }).catch((response) => {
      console.log('Error while getting grocery quantities.')
    })

    const groceryMeaninglessWordService = new GroceryMeaninglessWordService()
    groceryMeaninglessWordService.doGet().then((response) => {
      this.meaninglesswords = response
    }).catch((response) => {
      console.log('Error while getting grocery meaningless words.')
    })
  },
  methods: {
  }
}
</script>
