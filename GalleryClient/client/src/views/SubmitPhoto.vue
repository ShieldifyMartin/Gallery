<template>
  <div class="submit-photo">
    <h1>Submit a photo</h1>
    <p class="error">{{state.error}}</p>
    <form method="post" @submit.prevent="handleSubmit">
      <label for="file" class="photo-upload-label">choose a picture</label>
      <input type="file" id="file" class="photo-upload" ref="picture" @change="handleFileUpload" />
      <input type="text" v-model="state.location" name="location" placeholder="Location" />
      <input
        type="text"
        v-model="state.description"
        name="description"
        placeholder="Description"
        required
      />
      <select v-model="state.categoryId" class="categories-dropdown">
        <option disabled selected>-- select a category --</option>
        <option
          v-for="category in state.categories"
          :key="category.id"
          :value="category.title"
        >{{category.title}}</option>
      </select>
      <input type="submit" value="Send" class="submit-btn" />
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import router from "../router";
import { postService } from "../services";

export default defineComponent({
  setup() {
    const state = reactive({
      categories: [],
      picture: null,
      location: "",
      description: "",
      categoryId: null,
      error: "",
      maxSize: 15728640,
    });

    watchEffect(async () => {
      const categories = await postService.getCategories();
      state.categories = categories;
    });

    function handleFileUpload(e) {
      let files = e.target.files || e.dataTransfer.files;

      if (!files.length) return;

      if (files.length === 1) {
        if (files.size > state.maxSize) {
          state.error = "Too large picture!";
        } else {
          console.log(files[0]);
          state.picture = files[0];
        }
      } else {
        state.error = "Only one photo is allowed!";
      }
    }

    async function handleSubmit() {
      const { picture, location, description, categoryId } = state;
      const isCorrect = validate();

      if (!isCorrect) {
        return;
      }

      var response = await postService.create(
        picture,
        location,
        description,
        categoryId
      );

      if (response === 401) {
        router.push("/login");
      } else if (response === 400) {
        state.error = "Something went wrong!";
      } else {
        router.push("/");
      }
    }

    const validate = () => {
      if (state.picture === null || state.description.length === 0) {
        state.error = "Invalid date!";
        return false;
      }

      if (state.description.length > 2000) {
        state.error = "Max description length is 2000!";
        return false;
      }

      if (state.location.length > 40) {
        state.error = "Max location length is 40!";
        return false;
      }

      return true;
    };

    return {
      state,
      handleSubmit,
      handleFileUpload,
    };
  },
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

  .photo-upload::-webkit-file-upload-button {
    visibility: hidden;    
  }

  .photo-upload {    
    width: 25em;
    text-align: center;
  }

  .photo-upload-label {
    background-color: #eeeeee;
    color: #000;
    border: none;
    border-radius: 5px;    
    width: 20em;
    padding: 0.5em 1.5em;
  }

  .categories-dropdown {
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