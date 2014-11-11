/// <vs />
var gulp = require('gulp');
var concat = require('gulp-concat');
var concatsourcemap = require('gulp-concat-sourcemap');
var uglify = require('gulp-uglify');
var del = require('del');
var watch = require('gulp-watch');
var minifyCss = require('gulp-minify-css');
var concatCss = require('gulp-concat-css');
var ts = require('gulp-typescript');
var sourcemaps = require('gulp-sourcemaps');

var projectName = 'ScoreTracker.Web';

var config = {
    js: [
        'bower_components/jquery/jquery.js',
        'bower_components/angular/angular.js',
        'bower_components/angular-route/angular-route.js',
        'bower_components/angular-toastr/dist/angular-toastr.js',
        'bower_components/bootstrap/dist/js/bootstrap.js'
    ],
    ts: [
        projectName + '/app/*.ts',
        projectName + '/Scripts/typings/**/*.d.ts',
        projectName + '/app/**/*.ts'
    ],
    css: [
        'bower_components/bootstrap/dist/css/*.min.css',
        'bower_components/bootstrap/dist/css/*.map',
        'bower_components/angular-toastr/dist/*.min.css'
    ]
}

gulp.task('clean-js', function() {
    del.sync([projectName + '/app/all.min.js']);
});

gulp.task('clean-css', function () {
    del.sync([projectName + "/content/*.css", projectName + "/Content/*.map"]);
});

gulp.task('clean-ts', function () {
    del.sync([projectName + '/app/app.js']);
});

gulp.task('js', ['clean-js'], function() {

    gulp.src(config.js)
        //.pipe(uglify())
        .pipe(concat('all.min.js'))
        .on('error', swallowError)
        .pipe(gulp.dest(projectName + '/app/'));

});

gulp.task('css', ['clean-css'], function() {
    gulp.src(config.css)
        .pipe(gulp.dest(projectName + '/Content/'));
});

gulp.task('ts', ['clean-ts'], function() {
    gulp.src(config.ts)
        .pipe(sourcemaps.init())
        .pipe(ts({
            sortOutput: true,
            decarationFiles: true,
            noExternalResolve: true,
        }))
        .on('error', swallowError)
        .pipe(concatsourcemap('app.js'))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(projectName + '/app'));
});


gulp.task('watch', function () {
    console.log(typeof (config.ts));
    watch(config.js, function() {
        gulp.start('js');
    });
    watch(config.ts, function() {
        gulp.start('ts');
    });
    watch(config.css, function() {
        gulp.start('css');
    });

});

gulp.task('default', ['js', 'ts', 'css'], function () { });

function swallowError(error) {

    //If you want details of the error in the console
    console.log(error.toString());

    this.emit('end');
}

