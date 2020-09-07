<template>
  <div class="submit-photo">
    <h1>Submit a photo</h1>
    <form method="post" @submit.prevent="handleSubmit">            
      <input type="file" class="photo">
      <input type="text" v-model="location" name="location" placeholder="Location" />
      <input type="text" v-model="description" name="description" placeholder="Description" required />
      <select v-model="categoryId">
          <option disabled selected> -- select a category -- </option>
          <option v-for="category in state.categories" :key="category.id" value="category.title">
            {{category.title}}
          </option>
      </select>
      <input type="submit" value="Send" class="submit-btn" />
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from 'vue';
import { postService } from "../services";

export default defineComponent({
  setup() {
    const state = reactive({
      categories: [],
      picture: null,
      location: null,
      description: null,
      categoryId: null
    });

    watchEffect(async () => {
      const categories = await postService.getCategories();
      console.log(categories);
      state.categories = categories;
    })

    return {
      state,      
    }
  }    
});
</script>

<style lang="scss">
.submit-photo {
  * {
    display: block;
    margin: 1em auto;
  }

  .error {
    color: red;
  }

  input {
    width: 30em;
    height: 2em;
  }

  .photo {
    width: 30em;
  }

  .submit-btn {
    border: none;
    width: 30em;
    background: #111111;
    color: #ffff;
  }

  a {
    text-decoration: none;
    color: black;
  }

  a:hover {
    color: rgba(0, 0, 0, 0.467);
  }
}
</style>